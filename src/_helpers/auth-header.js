/*
Adam Bender
CST452 Mark Reha
2/6/22
Bearer token auth
*/

export function authHeader() {
    //return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}