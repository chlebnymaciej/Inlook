
import React, { useEffect } from "react";
import { Callback } from "react-oidc";
import userManager from "../Authorization/userManager";
import { useHistory } from "react-router";
import { User } from "oidc-client";

interface CallbackPageProps {
    setUser: React.Dispatch<React.SetStateAction<User | null>>;
}

const CallbackPage = (props: CallbackPageProps) => {
    const history = useHistory();
    // useEffect(()=>{
    //     sessionStorage.clear();
    //   },[])
    const handleSuccess = async () => {
        props.setUser(await userManager.getUser())
        history.push("/");
    };
    const handleError = () => {
        props.setUser(null);
        history.push("/");
    };
    return (
        <Callback userManager={userManager} onSuccess={handleSuccess} onError={handleError} />
    );
}

export default CallbackPage;