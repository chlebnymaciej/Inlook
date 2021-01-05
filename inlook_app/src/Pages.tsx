import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import {
  BrowserRouter,
  Route,
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
        <RoleRoute path="/newmessage" component={(props) => <NewMessage user={user} />} mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/contacts" component={ContactList} mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/inbox" component={Inbox} mustBeLogged user={user} requiredRoles={["User"]}/>
        <RoleRoute path="/groups" component={props => <Groups user={user} />} mustBeLogged user={user} requiredRoles={["User"]}/>
        <RoleRoute path="/groupinfo" component={GroupInfo}  mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/groupEdit"  component={EditGroup} mustBeLogged user={user} requiredRoles={["User"]} />
        <RoleRoute path="/creategroup" component={props => <CreateGroups user={user} />} mustBeLogged user={user} requiredRoles={["User"]}/>
        <RoleRoute path="/accounts" component={UserList} mustBeLogged user={user} requiredRoles={["Admin"]}/>
        <RoleRoute path="/callback" component={props =>  <CallbackPage setUser={setUser} />}/>
        <RoleRoute path="/waitingRoom"  component={WaitingRoom}/>
        <RoleRoute path="/" component={Home}/>
      </Switch>
    </BrowserRouter>
  </div>
  );
};
export default Pages;