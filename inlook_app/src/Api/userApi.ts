import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"

const targetUrl = BASE_URL + "user/"



export interface  UserModel {
    email: string;
    name:string;
    id: string;
}

export const getUsers = async () => {
    type T = IApiResponse<UserModel[]>;
    let url = targetUrl+"getUsersList";
    return fetch(url ,{
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
    totalCount: number;
    
}


export type OrderType = 'asc' | 'desc';

export const getContactList = async (page :number= 0, pageSize: number= 10, searchText?: string, orderBy?: keyof Contact, orderType?: OrderType ) => {
    type T = IApiResponse<ContactPageModel>;
    let url = targetUrl + "getContactList";
    url += `?page=${page}`;
    url += `&pageSize=${pageSize}`;
    if(searchText) url += `&searchText=${searchText}`;
    if(orderBy) url += `&orderBy=${orderBy}`;
    if(orderType) url += `&orderType=${orderType}`;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

 