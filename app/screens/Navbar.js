import React from "react";
import {
  SafeAreaView,
  Image,
  TouchableOpacity,
  StyleSheet,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";
import Colors from "../config/Colors";

const Navbar = ({ navigation }) => {
  const GoToUser = () => {
    navigation.navigate("User");
  };
  const GoToBenefits = () => {
    navigation.navigate("Benefits");
  };
  const GoToSettings = () => {
    navigation.navigate("Settings");
  };
  return (
    <SafeAreaView style={styles.navbar}>
      <TouchableOpacity style={styles.navbarButton} onPress={GoToUser}>
        <Ionicons name="person-outline" size={30} color={Colors.white} />
      </TouchableOpacity>
      <TouchableOpacity style={styles.navbarButton} onPress={GoToBenefits}>
        <Ionicons name="cash-outline" size={30} color={Colors.white} />
      </TouchableOpacity>
      <TouchableOpacity style={styles.navbarButton} onPress={GoToSettings}>
        <Ionicons name="settings-outline" size={30} color={Colors.white} />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  navbar: {
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",
    backgroundColor: Colors.primary,
    width: "100%",
    height: 60,
    position: "absolute",
    bottom: 0,
  },
  navbarButton: {
    justifyContent: "center",
    alignItems: "center",
  },
  navbarButtonImage: {
    width: 30,
    height: 30,
  },
});

export default Navbar;
