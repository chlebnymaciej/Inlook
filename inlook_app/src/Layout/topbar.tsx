import { Button } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { User } from "oidc-client";
import React from "react";
import { useHistory } from "react-router";
import userManager from "../Authorization/userManager";
import MenuButton from "./menu";


const useStyles = makeStyles(theme => ({
    topBar: {
      width: '100%',
      backgroundColor: "#74B9FF",
      height: "4em",
      display:"flex",
      justifyContent:"space-between"
    },
    loginDiv:{
        float: 'right',
        color: 'white',
        padding: '1em'
    },
    loginButton:{
        backgroundColor: "white",
        marginLeft: "1em",
        '&:hover':{
            backgroundColor:"#DAE0E2"
        }
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
            <MenuButton user={props.user}></MenuButton>
            <div className={classes.loginDiv}>
            {
                props.user ? 
                    <>{props.user.profile.name} <Button className={classes.loginButton} onClick={handleLogoutClick}>Wyloguj</Button></>
                        : 
                    <Button className={classes.loginButton} onClick={handleLoginClick}>Zaloguj</Button>
            }
            </div>
            
        </div>
    )
}

export default Topbar;