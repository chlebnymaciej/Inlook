
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
        localStorage.clear();
        const user = await userManager.getUser();
        if (user) {
            getUserRoles().then(r => {
                var roles = r.data || [];
                localStorage.setItem("roles", JSON.stringify(roles));
                props.setUser(user);
                if (roles.includes("User")) {
                    history.push("/");
                }
                else {
                    history.push("waitingRoom");
                }
            });
        }
        else {
            history.push("");
        }

    };
    const handleError = () => {
        localStorage.clear();
        props.setUser(null);
        enqueueSnackbar("Authorization error", { variant: "error" });
        history.push("");
    };
    return (
        <Callback userManager={userManager} onSuccess={handleSuccess} onError={handleError} />
    );
}

export default CallbackPage;