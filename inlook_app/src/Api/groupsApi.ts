import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"
import { UserModel } from "./userApi"

const url = BASE_URL + "group/"

export interface GroupModel {
    name: string
}

export interface CreateGroupModel {
    name: string,
    owner: UserModel,
    users: UserModel[],
}

export const getGroups = async () => {
    type T = IApiResponse<GroupModel[]>;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

export const postGroup  = async (group: CreateGroupModel) => {
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(url ,{
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify(group)
    }).then<T>(handleResponse).catch<T>(handleError);
}

 