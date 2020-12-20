
import { User } from "oidc-client";
import React, { PropsWithChildren, useEffect, useState } from "react";

export interface RoleRouteProps extends PropsWithChildren<JSX.Element> {
    component?: JSX.Element;
    requiredRoles?: string[];
    user: User;
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

    return (loggedAccess && roledAccess ? (props.component || props.children) :
        loggedAccess && !roledAccess ? <Waiting
    )
}