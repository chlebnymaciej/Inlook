import { Button } from "@material-ui/core";
import IconButton from "@material-ui/core/IconButton/IconButton";
import Menu from '@material-ui/core/Menu';
import MenuItem from "@material-ui/core/MenuItem/MenuItem";
import { makeStyles } from "@material-ui/core/styles";
import { AccountCircle } from "@material-ui/icons";
import { User } from "oidc-client";
import React, { useEffect } from "react";
import { useHistory } from "react-router";
import userManager from "../Authorization/userManager";
import MenuButton from "./menu";
const useStyles = makeStyles(theme => ({
    topBar: {
        width: '100%',
        backgroundColor: "#74B9FF",
        height: "4em",
        display: "flex",
        justifyContent: "space-between"
    },
    leftSide: {
        display: "flex",
        alignItems: "center"
    },
    loginDiv: {
        float: 'right',
        color: 'white',
        padding: '1em'
    },
    loginButton: {
        backgroundColor: "white",
        marginLeft: "1em",
        '&:hover': {
            backgroundColor: "#DAE0E2"
        }
    },
    appTitle: {
        margin: theme.spacing(1),
        color: "white"
    }


}));

interface ToolbarProps {
    user: User | null;
}

const Topbar = (props: ToolbarProps) => {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const history = useHistory();
    const [roles, setRoles] = React.useState<string[]>([]);


    const handleLoginClick = () => {
        userManager.signinRedirect();
    }
    const handleLogoutClick = () => {
        userManager.signoutRedirect();
    }

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };


    useEffect(() => {
        const roles = JSON.parse(localStorage.getItem('roles') || "[]");
        setRoles(roles || []);
    }, [props.user]);

    return (
        <div className={classes.topBar}>
            <div className={classes.leftSide}>
                {props.user && roles.includes("User") &&
                    <MenuButton user={props.user}></MenuButton>
                }
                <h1 className={classes.appTitle}>Inlook</h1>
            </div>
            <div className={classes.loginDiv}>
                {
                    props.user ?
                        <>{props.user.profile.name}
                            <IconButton
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="inherit"
                            >
                                <AccountCircle />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={open}
                                onClose={handleClose}
                            >
                                <MenuItem onClick={handleClose}>Profile</MenuItem>
                                <MenuItem onClick={(e) => { handleClose(); history.push('/myaccount'); }}>My account</MenuItem>
                                <MenuItem onClick={handleLogoutClick}>Log out</MenuItem>

                            </Menu>
                        </>
                        :
                        <Button className={classes.loginButton} onClick={handleLoginClick}>Log in</Button>
                }
            </div>

        </div>
    )
}

export default Topbar;