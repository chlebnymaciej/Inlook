import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import userManager from "../Authorization/userManager";

const Home = () => {
    const [name,setName] = useState<string>();

    useEffect(()=>{
        setUserInfo();
    },[]);

    const setUserInfo = async () => {
        
        const user:User | null= await  userManager.getUser();
        if(user === null){
            setName("Nieznajomy");
        }else{
            setName(user.profile.given_name);
        }

    }

    return <div>Witaj {name}</div>
}

export default Home;