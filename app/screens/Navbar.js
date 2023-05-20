import React from "react";
import {
  SafeAreaView,
  Image,
  TouchableOpacity,
  StyleSheet,
} from "react-native";

const Navbar = () => {
  return (
    <SafeAreaView style={styles.navbar}>
      <TouchableOpacity style={styles.navbarButton}></TouchableOpacity>
      <TouchableOpacity style={styles.navbarButton}></TouchableOpacity>
      <TouchableOpacity style={styles.navbarButton}></TouchableOpacity>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  navbar: {
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",
    backgroundColor: "#f2f2f2",
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
