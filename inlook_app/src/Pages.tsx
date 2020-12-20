import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import {
  BrowserRouter,
  Switch
} from "react-router-dom";
import userManager from "./Authorization/userManager";
import Topbar from "./Layout/topbar";
import CallbackPage from "./Pages/callbackPage";
import CreateGroups from "./Pages/groupCreate";
import GroupInfo from "./Pages/groupInfo";
import Groups from "./Pages/groups";
import Home from "./Pages/home";
import Inbox from "./Pages/Inbox/inbox";
import NewMessage from "./Pages/newmessage";
import ContactList from "./Pages/contactList";
import EditGroup from "./Pages/groupEdit";
import UserList from "./Pages/AdminPages/userList";
import WaitingRoom from "./Pages/waitingRoom";
import RoleRoute from "./RoleRoute";

const Pages = () => {
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    (async () => {
      setUser(await userManager.getUser());
    })();
  }, [])

  return (<div style={{ height: "100vh", display: "flex", flexDirection: "column" }}>
    <BrowserRouter>
      <Topbar user={user} />
      <Switch>
        <RoleRoute path="/newmessage" mustBeLogged user={user} requiredRoles={["User"]}>
          <NewMessage user={user} />
        </RoleRoute>
        <RoleRoute path="/contacts" mustBeLogged user={user} requiredRoles={["User"]}>
          <ContactList />
        </RoleRoute>
        <RoleRoute path="/inbox" mustBeLogged user={user} requiredRoles={["User"]}>
          <Inbox />
        </RoleRoute>
        <RoleRoute path="/groups" mustBeLogged user={user} requiredRoles={["User"]}>
          <Groups user={user} />
        </RoleRoute>
        <RoleRoute path="/groupinfo" component={GroupInfo} mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/groupEdit" component={EditGroup} mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/creategroup" mustBeLogged user={user} requiredRoles={["User"]}>
          <CreateGroups user={user} />
        </RoleRoute>
        <RoleRoute path="/accounts" mustBeLogged user={user} requiredRoles={["Admin"]}>
          <UserList />
        </RoleRoute>
        <RoleRoute path="/callback">
          <CallbackPage setUser={setUser} />
        </RoleRoute>
        <RoleRoute path="/waitingRoom">
          <WaitingRoom />
        </RoleRoute>
        <RoleRoute path="/">
          <Home />
        </RoleRoute>
      </Switch>
    </BrowserRouter>
  </div>
  );
};
export default Pages;