import React from "react";
import {
  SafeAreaView,
  StyleSheet,
  View,
  Dimensions,
  NativeModules,
  TouchableWithoutFeedback,
  Text,
} from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
const { StatusBarManager } = NativeModules;
import { useSetAtom } from "jotai";
import { logoutAtom } from "../store/AuthAtom";
import { Divider } from "@rneui/themed";
import { AntDesign } from "@expo/vector-icons";
import { MaterialIcons } from "@expo/vector-icons";

const STATUSBAR_HEIGHT = Platform.OS === "ios" ? 20 : StatusBarManager.HEIGHT;

const SettingScreen = ({ navigation }) => {
  const logout = useSetAtom(logoutAtom);
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.contentCointaner}>
        <TouchableWithoutFeedback
          onPress={() => {
            logout();
          }}
          style={styles.menuElement}
        >
          <View style={styles.menuElementContainer}>
            <Text style={styles.menuText}>Przycisk</Text>
            <AntDesign name="right" size={20} color={Colors.white} />
          </View>
        </TouchableWithoutFeedback>
        <Divider width={1} />
        <TouchableWithoutFeedback
          onPress={() => {
            logout();
          }}
          style={styles.menuElement}
        >
          <View style={styles.menuElementContainer}>
            <Text style={styles.menuText}>To jest przycisk</Text>
            <AntDesign name="right" size={20} color={Colors.white} />
          </View>
        </TouchableWithoutFeedback>
        <Divider width={1} />
        <TouchableWithoutFeedback
          onPress={() => {
            logout();
          }}
          style={styles.menuElement}
        >
          <View style={styles.menuElementContainer}>
            <Text style={styles.menuText}>To też jest przycisk ez</Text>
            <AntDesign name="right" size={20} color={Colors.white} />
          </View>
        </TouchableWithoutFeedback>
        <Divider width={1} />

        <TouchableWithoutFeedback
          onPress={() => {
            logout();
          }}
          style={styles.menuElement}
        >
          <View style={styles.menuElementContainer}>
            <Text style={styles.logoutText}>Wyloguj Się!</Text>
            <MaterialIcons name="logout" size={24} color={Colors.errorRed} />
          </View>
        </TouchableWithoutFeedback>
        <Divider width={1} />
      </View>
      <Navbar navigation={navigation} />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.secondary,
  },
  menuElementContainer: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
  },
  contentCointaner: {
    marginTop: STATUSBAR_HEIGHT,
    height: Dimensions.get("screen").height,
    width: Dimensions.get("screen").width,
    flex: 1,
    paddingTop: 10,
    paddingLeft: 15,
    paddingRight: 15,
  },
  menuElement: {},
  logoutText: {
    color: Colors.errorRed,
    fontSize: 20,
    padding: 7,
    paddingLeft: 12,
    paddingBottom: 17,
    marginTop: 10,
  },
  menuText: {
    color: Colors.white,
    fontSize: 20,
    padding: 7,
    paddingLeft: 12,
    paddingBottom: 17,
    marginTop: 10,
  },
});

export default SettingScreen;
