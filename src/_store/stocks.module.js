/*
Adam Bender
CST452 Mark Reha
2/6/22
Vuex store for stock functions
*/

import {positionService} from '../_services';
import {router} from "../_helpers";

const state = {
    all: {}
};

const actions = {
    getAll({ commit }, userid) {
        commit('getAllRequest', userid);

        positionService.getStock(userid)
            .then(
                stocks => commit('getAllSuccess', stocks),
                error => commit('getAllFailure', error)
            );
    }
};

const mutations = {
    getAllRequest(state) {
        state.all = { loading: true };
    },
    getAllSuccess(state, stocks) {
        state.all = { items: stocks };
    },
    getAllFailure(state, error) {
        state.all = { error };
    }
};

export const stocks = {
    namespaced: true,
    state,
    actions,
    mutations
};
