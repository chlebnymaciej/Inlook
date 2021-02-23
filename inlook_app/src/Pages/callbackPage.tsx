
import React, { useEffect, useState } from "react";
import { Callback } from "react-oidc";
import userManager from "../Authorization/userManager";
import { useHistory } from "react-router";
import { User } from "oidc-client";
import { useSnackbar } from "notistack";
import { getUserRoles } from "../Api/userApi";
import { maxHeaderSize } from "http";
import AnnaClockGif from "../Resources/anna_clock.gif";
import FrozenRedirectText from "../Resources/frozen_redirect_text.png";
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    gif: {
        marginLeft: "auto",
        marginRight: "auto",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center"
    }
}));

interface CallbackPageProps {
    setUser: React.Dispatch<React.SetStateAction<User | null>>;
}

const CallbackPage =React.memo( (props: CallbackPageProps) => {
    const history = useHistory();
    const classes = useStyles();

    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    useEffect(()=>{
        userManager.signinRedirectCallback().then(async user => {
            if(user)
            {
                const response = await getUserRoles();
                var roles = response.data || [];
                localStorage.setItem("roles", JSON.stringify(roles));
                props.setUser(user);
                if (roles.includes("User")) {
                    history.push("/");
                }
                else {
                    history.push("waitingRoom");
                }
            }
            else
            {
                localStorage.removeItem("roles");
                props.setUser(null);
                enqueueSnackbar("Authorization error", { variant: "error" });
                history.push("");
            }
        })
    },[]);

    return (
        <>
        <img src={FrozenRedirectText} className={classes.gif} />
        <img src={AnnaClockGif} className={classes.gif} />
        </>
        
    );
});

export default CallbackPage;