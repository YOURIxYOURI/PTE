import { StatusBar } from "expo-status-bar";
import { StyleSheet, Text, View } from "react-native";
import Navigation from "./components/Navigation";
import Colors from "./config/Colors";

export default function App() {
  return (
    <View style={{ flex: 1, backgroundColor: Colors.secondary }}>
      <StatusBar barStyle="light-content" />
      <Navigation />
    </View>
  );
}
