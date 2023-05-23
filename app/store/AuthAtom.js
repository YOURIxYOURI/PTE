import { atom } from "jotai";

export const isAuthenticatedAtom = atom(false)
export const userDataAtom = atom(null)

export const loginAtom = atom(null, (get, set, update) => {
    set(userDataAtom, update)
    set(isAuthenticatedAtom, true)
})

export const logoutAtom = atom(null, (get, set) => {
    set(userDataAtom, null)
    set(isAuthenticatedAtom, false)
})