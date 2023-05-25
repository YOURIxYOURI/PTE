import { StatusBar } from "expo-status-bar";
import { StyleSheet, Text, View } from "react-native";
import React, { useContext } from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import LoginScreen from "../screens/LoginScreen";
import RegisterScreen from "../screens/RegisterScreen";
import UserScreen from "../screens/UserScreen";
import SettingScreen from "../screens/SettingScreen";
import BenefitsScreen from "../screens/BenefitsScreen";
import BenefitDetailsScreen from "../screens/BenefitDetailsScreen";
import { useAtomValue } from "jotai";
import { isAuthenticatedAtom } from "../store/AuthAtom";

const Stack = createNativeStackNavigator();

const AuthNavigator = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{
          headerShown: false,
          gestureEnabled: false,
        }}
      >
        <Stack.Screen
          name="Login"
          component={LoginScreen}
          options={{ gestureEnabled: false}}
        />
        <Stack.Screen
          name="Register"
          component={RegisterScreen}
          options={{ gestureEnabled: false }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

const AppNavigator = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{
          headerShown: false,
          gestureEnabled: false,
        }}
      >
        <Stack.Screen
          name="Benefits"
          component={BenefitsScreen}
          options={{ gestureEnabled: false }}
        />
        <Stack.Screen
          name="User"
          component={UserScreen}
          options={{ gestureEnabled: false }}
        />
        <Stack.Screen
          name="Settings"
          component={SettingScreen}
          options={{ gestureEnabled: false }}
        />
        <Stack.Screen
          name="BenefitDetails"
          component={BenefitDetailsScreen}
          options={{ gestureEnabled: false }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default function Navigation() {
  const isAuthenticated = useAtomValue(isAuthenticatedAtom);
  return <>{isAuthenticated ? <AppNavigator /> : <AuthNavigator />}</>;
}
