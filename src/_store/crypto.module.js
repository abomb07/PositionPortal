/*
Adam Bender
CST452 Mark Reha
2/6/22
Vuex store for crypto get all functions
*/

import {positionService} from '../_services';
import {router} from "../_helpers";

const state = {
    all: {}
};

const actions = {
    getAll({ commit }, userid) {
        commit('getAllRequest', userid);

        positionService.getCrypto(userid)
            .then(
                crypto => commit('getAllSuccess', crypto),
                error => commit('getAllFailure', error)
            );
    }
};

const mutations = {
    getAllRequest(state) {
        state.all = { loading: true };
    },
    getAllSuccess(state, crypto) {
        state.all = { items: crypto };
    },
    getAllFailure(state, error) {
        state.all = { error };
    }
};

export const crypto = {
    namespaced: true,
    state,
    actions,
    mutations
};
