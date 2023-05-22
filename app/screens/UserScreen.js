import React, { useContext } from "react";
import { SafeAreaView, StyleSheet } from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { AuthContext } from "../components/AuthContext";

const UserScreen = ({ navigation }) => {
  const { isAuthenticated, userData, login } = useContext(AuthContext);
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
