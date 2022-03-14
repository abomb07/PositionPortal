/*
Adam Bender
CST452 Mark Reha
2/6/22
Vuex store for position detail functions
*/

import { positionService } from '../_services';
import {router} from "../_helpers";

const quote = JSON.parse(localStorage.getItem('quote'));
const state = quote
    ? { status: { hasQuote: true }, quote }
    : { status: {}, quote: null };

const actions = {
    getTotalQuote({ commit }, userid) {
        commit('getTotalQuoteRequest', userid);

        positionService.getTotalQuote(userid)
            .then(
                positionsDetail => commit('getTotalQuoteSuccess', positionsDetail),
                error => commit('getTotalQuoteFailure', error)
            );
    },
    getQuote({ commit }, {userid, quoteName, quoteType}) {
        commit('getQuoteRequest', userid);

        positionService.getQuote(userid, quoteName, quoteType)
            .then(
                quote => {
                    commit('getQuoteSuccess', quote);
                    router.push('/detail');
                },
                error => commit('getQuoteFailure', error)
            );
    }
};

const mutations = {
    getTotalQuoteRequest(state) {
        state.all = { loading: true };
    },
    getTotalQuoteSuccess(state, positionsDetail) {
        state.all = { items: positionsDetail };
    },
    getTotalQuoteFailure(state, error) {
        state.all = { error };
    },
    getQuoteRequest(state) {
        state.all = { loading: true };
    },
    getQuoteSuccess(state, quote) {
        state.status = { hasQuote: true};
        state.quote = quote;
    },
    getQuoteFailure(state, error) {
        state.all = { error };
    }
};

export const positionsDetail = {
    namespaced: true,
    state,
    actions,
    mutations
};
