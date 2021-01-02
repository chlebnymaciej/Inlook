import { makeStyles } from "@material-ui/core";
import React, { useEffect } from "react";
import { getUserRoles } from "../Api/userApi";
import inlookLogo from "./../Resources/sonicWaiting.gif";
import { useHistory } from "react-router";

const useStyles = makeStyles(theme => ({
    header_my: {
        margin: "auto",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center"
    }
}));

const WaitingRoom = () => {
    const classes = useStyles();
    const history = useHistory();

    useEffect(() => {
        getUserRoles().then(r => {
            var roles = r.data || [];
            localStorage.setItem("roles", roles.toLocaleString());
            if (roles.includes("User")) {
                history.push("/");
            }
        })
    }, []);

    return <div className={classes.header_my} >
        <h1>Hello!</h1>
        <h3>Wait for the admin to confirm your account </h3>
        <img src={inlookLogo} />
    </div>

}

export default WaitingRoom;