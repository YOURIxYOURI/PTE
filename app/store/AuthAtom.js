import { atom } from "jotai";
import { Platform } from "react-native";

export const isAuthenticatedAtom = atom(false);
export const userDataAtom = atom(null);
export const benefitIdAtom = atom(0);

export const loginAtom = atom(null, (get, set, update) => {
  set(userDataAtom, update);
  set(isAuthenticatedAtom, true);
});

export const setIdAtom = atom(null, (get, set, update) => {
  set(benefitIdAtom, update);
});

export const getIdAtom = atom(null, (get, set, update) => {
  get(benefitIdAtom);
});

export const logoutAtom = atom(null, (get, set) => {
  set(userDataAtom, null);
  set(isAuthenticatedAtom, false);
});
