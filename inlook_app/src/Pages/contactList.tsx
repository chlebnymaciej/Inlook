import { Checkbox, createStyles, IconButton, makeStyles, Paper, Table, TableCell, TableHead, TablePagination, TableRow, TableSortLabel, TextField, Theme } from '@material-ui/core';
import CommentIcon from '@material-ui/icons/Comment';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import * as userApi from '../Api/userApi';
import { Contact, OrderType } from '../Api/userApi';

export interface ContactListProps {

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
  id: keyof Contact;
  label: string;
}

const headCells: HeadCell[] = [
  { id: 'name', label: 'Name' },
  { id: 'email', label: 'Email' },
];

const ContactList = (props: ContactListProps) => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const classes = useStyles();

  const [contacts, setContacts] = useState<Contact[]>();
  const [error, setError] = useState<string>();
  const [checked, setChecked] = React.useState<string[]>([]);
  const [page, setPage] = React.useState<number>(0);
  const [totalCount, setTotalCount] = React.useState<number>(0);
  const [searchText, setSearchText] = React.useState<string>("");
  const [searchTextTimeout, setSearchTextTimeout] = React.useState<NodeJS.Timeout>();
  const [orderBy, setOrderBy] = React.useState<keyof Contact>('name');
  const [orderType, setOrderType] = React.useState<OrderType>('asc');
  const [rowsPerPage, setRowsPerPage] = React.useState(5);

  useEffect(() => {
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

  const getPage = (pageNumber: number, pageSize: number, searchText: string, orderBy?: keyof Contact, orderType?: OrderType) => {
    userApi.getContactList(pageNumber, pageSize, searchText, orderBy, orderType)
      .then(r => {
        if (r.isError) {
          enqueueSnackbar("Could not load contact list", { variant: "error" });
          return;
        }
        setContacts(r.data?.contacts);
        setTotalCount(r.data?.totalCount || 0)
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

  const handleTableHeadClick = (event: React.MouseEvent<unknown>, property: keyof Contact) => {
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
    <>
      {!error && <Paper className={classes.root}>
        <TextField onChange={handleSearchTextChange} className={classes.searchField} label={"Wyszukaj"} />
        <Table >

          {tableHeadWidths}
          {tableHead}

          {contacts?.map((contact, index) => {
            const labelId = `checkbox-list-label-${contact.email}`;

            return (
              <TableRow key={index} >
                <TableCell >
                </TableCell>
                <TableCell>
                  <p>{contact.name}</p>
                </TableCell>
                <TableCell>
                  <p>{contact.email}</p>
                </TableCell>
                <TableCell>
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
      </Paper>}
      {error}
    </>
  );

}
export default ContactList;