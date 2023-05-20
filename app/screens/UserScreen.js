import React from "react";
import { SafeAreaView, StyleSheet } from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "./Navbar";

const UserScreen = () => {
  return (
    <SafeAreaView>
      <navbar />
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
