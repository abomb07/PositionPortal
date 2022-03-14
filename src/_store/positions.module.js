/*
Adam Bender
CST452 Mark Reha
2/6/22
Vuex store for position functions
*/

import {positionService} from '../_services';
import {router} from "../_helpers";

const state = {
    all: {}
};

const actions = {
    getAll({ commit }, userid) {
        commit('getAllRequest', userid);

        positionService.getAll(userid)
            .then(
                positions => commit('getAllSuccess', positions),
                error => commit('getAllFailure', error)
            );
    },
    insert({ dispatch, commit }, position) {
        commit('insertRequest', position);

        positionService.insert(position)
            .then(
                position => {
                    commit('insertSuccess', position);
                    router.push('/');
                    setTimeout(() => {
                        //display success message after route change completes
                        dispatch('alert/success', 'Position added successfully', { root: true });
                    })
                },
                error => {
                    commit('insertFailure', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    }
};

const mutations = {
    getAllRequest(state) {
        state.all = { loading: true };
    },
    getAllSuccess(state, positions) {
        state.all = { items: positions };
    },
    getAllFailure(state, error) {
        state.all = { error };
    },
    insertRequest(state, position) {
        state.status = { inserting: true };
    },
    insertSuccess(state, position) {
        state.status = {};
    },
    insertFailure(state, error) {
        state.status = {};
    }
};

export const positions = {
    namespaced: true,
    state,
    actions,
    mutations
};
