import React, { useContext } from "react";
import { SafeAreaView, StyleSheet } from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { useAtomValue } from "jotai";
import { userDataAtom } from "../store/AuthAtom";

const UserScreen = ({ navigation }) => {
  // const { isAuthenticated, userData, login } = useContext(AuthContext);
  const user = useAtomValue(userDataAtom);
  return (
    <SafeAreaView style={styles.container}>
      <Navbar navigation={navigation} />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.secondary,
    justifyContent: "center",
    alignItems: "center",
  },
});

export default UserScreen;
