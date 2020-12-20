
import { User } from "oidc-client";
import React, { PropsWithChildren, useEffect, useState } from "react";
import { Route } from "react-router";
import Home from "./Pages/home";
import WaitingRoom from "./Pages/waitingRoom";

export interface RoleRouteProps extends PropsWithChildren<any> {
    path: string;
    component?: JSX.Element | ((props: any) => JSX.Element);
    requiredRoles?: string[];
    user?: User | null;
    mustBeLogged?: boolean;
}

const RoleRoute = (props: RoleRouteProps) => {

    const [loggedAccess, setLoggedAccess] = useState<boolean>(false);
    const [roledAccess, setRoleAccess] = useState<boolean>(false);

    useEffect(() => {
        if (props.mustBeLogged) {
            if (props.user) {
                setLoggedAccess(true)
            }
            else {
                setLoggedAccess(false)
            };
        }
        else {
            setLoggedAccess(true)
        }

    }, [props.user]);

    useEffect(() => {
        const roles = JSON.parse(localStorage.getItem('roles') || "[]") as string[];
        let isRoleAccess: boolean = true;
        props.requiredRoles?.forEach(rr => {
            if (!roles.includes(rr)) {
                isRoleAccess = false;
            }
        });
        setRoleAccess(isRoleAccess);

    }, [props.user]);

    return (
        <Route path={props.path}>
            { (loggedAccess && roledAccess) ? (props.component || props.children) :
                (loggedAccess && !roledAccess) ? <WaitingRoom /> : <Home />
            }
        </Route>)

}
export default RoleRoute;