import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { getWeather, WeatherForecast } from "../Api/weatherApi";
import userManager from "../Authorization/userManager";

interface HomeProps {
    user: User | null;
}

const Home = (props: HomeProps) => {
    const [error,setError] = useState<string>();
    const [weather,setWeather] = useState<WeatherForecast[]>();

    useEffect(()=>{
        getWeather().then(result => {
            if(result.isError){
                setError(result.errorMessage);
            }
            else{
                setWeather(result.data);
            }
        })
    },[props.user]);



    return <>
        {error ? 
            <p>{error}</p>
            :
            <div>
                {weather?.map((w,index) => <div key={index}>
                        <p>{w?.dateTime}</p>
                        <p>{w?.summary}</p>
                        <p>{w?.temperatureC}</p>
                    </div>)
                }
            </div>
        }
    </>
    
}

export default Home;