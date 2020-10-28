import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"

const url = BASE_URL + "weatherforecast/"

export interface  WeatherForecast {
     dateTime: Date 
      temperatureC: number
      summary: string
}

export const getWeather = async () => {
    type T = IApiResponse<WeatherForecast[]>;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

 