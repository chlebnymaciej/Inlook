import getUserToken from "../Authorization/getUserToken"

const url = BASE_URL + "weatherforecast"

export const getWeather = async () => {
    fetch(url,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    })
}

 