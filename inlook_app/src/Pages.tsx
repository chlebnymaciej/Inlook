import { User } from "oidc-client";
import React, { useState } from "react";
import {
  BrowserRouter,
  Switch,
  Route,
} from "react-router-dom";
import Topbar from "./Layout/topbar";
import CallbackPage from "./Pages/callbackPage";
import Home from "./Pages/home";

const Pages = () =>  {
  const [user,setUser] = useState<User |null>(null)
    return (<>
      <Topbar user={user}/>
      <BrowserRouter>
        <Switch>
        <Route path="/callback">
            <CallbackPage setUser={setUser}/>
        </Route>
        <Route path="/">
            <Home />
        </Route>
      </Switch>
    </BrowserRouter>
    </>
    );
  };
  export default Pages;