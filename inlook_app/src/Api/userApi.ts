import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"

const targetUrl = BASE_URL + "user/"



export interface  UserModel {
    mail: string 
}

export const getUsers = async () => {
    type T = IApiResponse<UserModel[]>;
    return fetch(targetUrl ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

export interface Contact {
    email: string;
    name: boolean;
    phoneNumber: boolean;
}

export interface  ContactPageModel {
    contacts: Contact[];
    totalPages: number;
    
}

export const getContactList = async (page :number= 1, pageSize: number= 10, searchText: string) => {
    type T = IApiResponse<ContactPageModel>;
    let url = targetUrl + "getContactList";
    url += `?page=${page}`;
    url += `&pageSize=${pageSize}`;
    url += `&searchText=${searchText}`;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

 