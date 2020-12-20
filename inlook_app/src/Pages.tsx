import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import {
  BrowserRouter,
  Switch,
  Route,
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
        <Route path="/newmessage">
          <NewMessage user={user} />
        </Route>
        <Route path="/contacts">
          <ContactList />
        </Route>
        <Route path="/inbox">
          <Inbox />
        </Route>
        <Route path="/groups">
          <Groups user={user} />
        </Route>
        <Route path="/groupinfo" component={GroupInfo} />
        <Route path="/groupEdit" component={EditGroup} />
        <Route path="/creategroup">
          <CreateGroups user={user} />
        </Route>
        <Route path="/accounts">
          <UserList />
        </Route>
        <Route path="/callback">
          <CallbackPage setUser={setUser} />
        </Route>
        <Route path="/waitingRoom">
          <WaitingRoom />
        </Route>
        <Route path="/">
          <Home />
        </Route>
      </Switch>
    </BrowserRouter>
  </div>
  );
};
export default Pages;