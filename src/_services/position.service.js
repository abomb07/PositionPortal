/*
Adam Bender
CST452 Mark Reha
2/6/22
Consumes backend api endpoints
*/

import config from 'config';

export const positionService = {
    insert,
    getAll,
    getTotalQuote,
    getStock,
    getCrypto,
    getQuote
};

function insert(position) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(position)
    };

    return fetch(`${config.apiUrl}/position/insert`, requestOptions).then(handleResponse);
}

function getAll(userid) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${config.apiUrl}/position/names/${userid}`, requestOptions).then(handleResponse);
}

function getTotalQuote(userid) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${config.apiUrl}/position/quote/${userid}`, requestOptions).then(handleResponse);
}

function getStock(userid) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${config.apiUrl}/position/names/stock/${userid}`, requestOptions).then(handleResponse);
}

function getCrypto(userid) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${config.apiUrl}/position/names/crypto/${userid}`, requestOptions).then(handleResponse);
}

function getQuote(userid, quoteName, quoteType) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${config.apiUrl}/position/quote/${userid}/${quoteName}/${quoteType}`, requestOptions)
        .then(handleResponse)
        .then(quote => {
            if (quote.quantity) {
                //store quote in local storage
                localStorage.setItem('quote', JSON.stringify(quote));
            }

            return quote;
        });
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                //auto logout if 401 response returned from api
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}