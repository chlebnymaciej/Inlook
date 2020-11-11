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
import Home from "./Pages/home";
import Inbox from "./Pages/inbox";
import NewMessage from "./Pages/newmessage";
import ContactList from "./Pages/contactList";

const Pages = () =>  {
  const [user,setUser] = useState<User | null>(null)

  useEffect(()=>{
    (async () => {
      setUser(await userManager.getUser())
    })();
  },[])

    return (<div style={{height:"100vh", display:"flex", flexDirection:"column"}}>
      <BrowserRouter>
        <Topbar user={user} />
        <Switch>
        <Route path="/callback">
            <CallbackPage setUser={setUser}/>
        </Route>
        <Route path="/newmessage">
          <NewMessage user={user}/>
        </Route>
        <Route path="/contacts">
          <ContactList />
        </Route>
        <Route path="/inbox">
        <Inbox/>
        </Route>
        <Route path="/">
            <Inbox></Inbox>
        </Route>
      </Switch>
    </BrowserRouter>
    </div>
    );
  };
  export default Pages;