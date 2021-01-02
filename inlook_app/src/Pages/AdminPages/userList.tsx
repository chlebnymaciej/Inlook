import { Checkbox, createStyles, IconButton, makeStyles, Paper, Table, TableCell, TableHead, TablePagination, TableRow, TableSortLabel, TextField, Theme } from "@material-ui/core";
import { useSnackbar } from "notistack";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router";
import * as userApi from "../../Api/userApi";
import { OrderType, AccountModel } from "../../Api/userApi";
import DeleteIcon from '@material-ui/icons/Delete';
import TouchAppIcon from '@material-ui/icons/TouchApp';
import shape from "@material-ui/core/styles/shape";
//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
export interface UserListProps {

}
const sgMail = require("@sendgrid/mail");
var SENDGRID_API_KEY = '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';
sgMail.setApiKey(SENDGRID_API_KEY);
const msg ={

    to:"kubakrolik99@gmail.com",
    from:"kubakrolik99@gmail.com",
    subject:"XD",
    text:"POZDERKI",
    html:"<strong>xD</strong>",
};
//console.log({ key: (SENDGRID_API_KEY) });
sgMail.send(msg);
const welcomeMail = (user: userApi.AccountModel ) =>
{
    sgMail.send(msg);
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: 'max-content',
            minWidth: '30%',
            backgroundColor: theme.palette.background.paper,
        },
        searchField: {
            width: "30%",
            margin: theme.spacing(2),
        },
        visuallyHidden: {
            border: 0,
            clip: 'rect(0 0 0 0)',
            height: 1,
            margin: -1,
            overflow: 'hidden',
            padding: 0,
            position: 'absolute',
            top: 20,
            width: 1,
        },
    }),
);

interface HeadCell {
    id: keyof AccountModel;
    label: string;
}

const headCells: HeadCell[] = [
    { id: 'name', label: 'Name' },
    { id: 'email', label: 'Email' },
    { id: 'accepted', label: 'Accepted' }
];

const UserList = (props: UserListProps) => {
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
    const history = useHistory();
    const classes = useStyles();

    const [roles, setRoles] = useState<string[]>([]);
    const [users, setUsers] = useState<AccountModel[]>([]);
    const [orderBy, setOrderBy] = React.useState<keyof AccountModel>('name');
    const [orderType, setOrderType] = React.useState<OrderType>('asc');
    const [page, setPage] = React.useState<number>(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    const [searchText, setSearchText] = React.useState<string>("");
    const [searchTextTimeout, setSearchTextTimeout] = React.useState<NodeJS.Timeout>();
    const [checked, setChecked] = React.useState<string[]>([]);
    const [totalCount, setTotalCount] = React.useState<number>(0);

    useEffect(() => {
        const roles = JSON.parse(localStorage.getItem('roles') || "[]");
        setRoles(roles || []);
        if (!roles.includes("Admin")) {
            enqueueSnackbar("Unauthorized", { variant: "error" });
            history.push("/");
        }

        getPage(page, rowsPerPage, searchText, orderBy, orderType);

    }, []);

    const handleChangePage = (event: React.MouseEvent<HTMLButtonElement, MouseEvent> | null, page: number) => {
        setPage(page);
        getPage(page, rowsPerPage, searchText, orderBy, orderType);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        const pageSize = parseInt(event.target.value, 10);
        setRowsPerPage(pageSize);
        getPage(page, pageSize, searchText, orderBy, orderType);
    };

    const getPage = (pageNumber: number, pageSize: number, searchText: string, orderBy?: keyof AccountModel, orderType?: OrderType) => {
        userApi.getAccounts(pageNumber, pageSize, searchText, orderBy, orderType)
            .then(r => {
                if (r.isError) {
                    enqueueSnackbar("Could not load contact list", { variant: "error" });
                    return;
                }
                setUsers(r.data ? r.data.accounts : []);
                setTotalCount(r.data?.totalCount || 0);
            })
    }


    const handleToggle = (mail: string) => {
        const currentIndex = checked.indexOf(mail);
        const newChecked = [...checked];

        if (currentIndex === -1) {
            newChecked.push(mail);
        } else {
            newChecked.splice(currentIndex, 1);
        }

        setChecked(newChecked);
    };

    const handleSearchTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setSearchText(value);
        if (searchTextTimeout) clearTimeout(searchTextTimeout);
        let newTimeout = setTimeout(() => {
            getPage(page, rowsPerPage, value, orderBy, orderType);
        }, 300);
        setSearchTextTimeout(newTimeout);

    }

    const handleTableHeadClick = (event: React.MouseEvent<unknown>, property: keyof AccountModel) => {
        let newOrderType: OrderType;
        if (orderBy === property) {
            newOrderType = orderType === "asc" ? "desc" : "asc";
        }
        else {
            newOrderType = "asc";
        }

        setOrderType(newOrderType);
        setOrderBy(property);

        getPage(page, rowsPerPage, searchText, property, newOrderType);

    }

    const acceptUser = (userId: string) => {
        userApi.acceptUser(userId, true)
            .then(r => {
                if (r.isError) {
                    enqueueSnackbar("Something went wrong", { variant: "error" });
                }
                else {                  
                    setUsers(prev => prev.map(u => {
                        welcomeMail(u);
                        if (u.id === userId) {
                            return { ...u, accepted: true };
                        }
                        return u;
                    }))
                }
            })
    }

    const deleteUser = (userId: string) => {
        userApi.deleteUser(userId).then(r => {
            if (r.errorMessage) {
                enqueueSnackbar("Cannot delete user", { variant: "error" });
            }
            else {
                setUsers(users.filter(u => u.id !== userId));
            }
        })
    }

    const tableHeadWidths = <colgroup>
        <col style={{ width: '10%' }} />
        <col style={{ width: '40%' }} />
        <col style={{ width: '40%' }} />
        <col style={{ width: '10%' }} />
    </colgroup>

    const tableHead =
        <TableHead  >
            <TableCell></TableCell>
            {headCells.map(hc => {
                return <TableCell
                    key={hc.id}
                    sortDirection={orderBy === hc.id ? orderType : false}
                >
                    <TableSortLabel
                        active={orderBy === hc.id}
                        direction={orderBy === hc.id ? orderType : 'asc'}
                        onClick={(event: React.MouseEvent<unknown>) => handleTableHeadClick(event, hc.id)}
                    >
                        {hc.label}
                        {orderBy === hc.id ? (
                            <span className={classes.visuallyHidden}>
                                {orderBy === hc.id ? 'sorted descending' : 'sorted ascending'}
                            </span>
                        ) : null}
                    </TableSortLabel>
                </TableCell>
            })}
        </TableHead>

    return (
        <Paper className={classes.root}>
            <TextField onChange={handleSearchTextChange} className={classes.searchField} label={"Wyszukaj"} />
            <Table >

                {tableHeadWidths}
                {tableHead}

                {users?.map((user, index) => {
                    const labelId = `checkbox-list-label-${user.email}`;

                    return (
                        <TableRow key={index} >
                            <TableCell >
                                <Checkbox
                                    edge="start"
                                    checked={checked.indexOf(user.email) !== -1}
                                    tabIndex={-1}
                                    disableRipple
                                    inputProps={{ 'aria-labelledby': labelId }}
                                    onChange={() => handleToggle(user.email)}
                                />
                            </TableCell>
                            <TableCell>
                                <p>{user.name}</p>
                            </TableCell>
                            <TableCell>
                                <p>{user.email}</p>
                            </TableCell>
                            {
                                user.accepted ?
                                    <TableCell />
                                    :
                                    <IconButton edge="end" aria-label="comments" onClick={() => acceptUser(user.id)} >
                                        <TouchAppIcon />
                                    </IconButton>
                            }
                            <TableCell>
                                <IconButton edge="end" aria-label="comments" onClick={() => deleteUser(user.id)}>
                                    <DeleteIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    );
                })}
            </Table>

            <TablePagination
                count={totalCount}
                page={page}
                rowsPerPage={rowsPerPage}
                rowsPerPageOptions={[2, 5, 10, 25]}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
            />
        </Paper>
    )

}

export default UserList;