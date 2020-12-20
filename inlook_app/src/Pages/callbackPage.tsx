
import React, { useEffect } from "react";
import { Callback } from "react-oidc";
import userManager from "../Authorization/userManager";
import { useHistory } from "react-router";
import { User } from "oidc-client";
import { useSnackbar } from "notistack";
import { getUserRoles } from "../Api/userApi";
import { maxHeaderSize } from "http";

interface CallbackPageProps {
    setUser: React.Dispatch<React.SetStateAction<User | null>>;
}

const CallbackPage = (props: CallbackPageProps) => {
    const history = useHistory();

    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const handleSuccess = async () => {
        props.setUser(await userManager.getUser());
        getUserRoles().then(r => {
            var roles = r.data || [];
            localStorage.setItem("roles", JSON.stringify(roles));
            if (roles.includes("User")) {
                history.push("/");
            }
            else {
                history.push("waitingRoom");
            }
        });

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