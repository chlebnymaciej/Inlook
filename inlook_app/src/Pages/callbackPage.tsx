
import React, { useEffect } from "react";
import { Callback } from "react-oidc";
import userManager from "../Authorization/userManager";
import { useHistory } from "react-router";
import { User } from "oidc-client";
import { useSnackbar } from "notistack";

interface CallbackPageProps {
    setUser: React.Dispatch<React.SetStateAction<User | null>>;
}

const CallbackPage = (props: CallbackPageProps) => {
    const history = useHistory();

    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const handleSuccess = async () => {
        props.setUser(await userManager.getUser())
        history.push("/");
    };
    const handleError = () => {
        props.setUser(null);
        enqueueSnackbar("Authorization error", { variant: "error" });
    };
    return (
        <Callback userManager={userManager} onSuccess={handleSuccess} onError={handleError} />
    );
}

export default CallbackPage;