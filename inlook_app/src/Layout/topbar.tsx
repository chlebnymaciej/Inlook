import { Button } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { User } from "oidc-client";
import React from "react";
import userManager from "../Authorization/userManager";


const useStyles = makeStyles(theme => ({
    topBar: {
      width: '100%',
      backgroundColor: "blue",
      height: "40px",
    },
    loginDiv:{
        float: 'right',
    },
    loginButton:{
        backgroundColor: "white",
    },
    logoutButton:{
        backgroundColor: "white",
    }

  }));

  interface ToolbarProps {
      user: User | null;
  }
    
const Topbar = (props: ToolbarProps) => {
    const classes  = useStyles();
  

    const handleLoginClick = () => {
        userManager.signinRedirect();
    }
    const handleLogoutClick = () => {
        userManager.signoutRedirect();
    }

    return (
        <div className={classes.topBar}>
            <div className={classes.loginDiv}>
            {
                props.user ? 
                    <>{props.user.profile.name} <Button className={classes.loginButton} onClick={handleLogoutClick}>Wyloguj</Button></>
                        : 
                    <Button className={classes.logoutButton} onClick={handleLoginClick}>Zaloguj</Button>
            }
            </div>
            
        </div>
    )
}

export default Topbar;