import React, { useState } from "react";
import {
  View,
  TextInput,
  Button,
  Alert,
  StyleSheet,
  Text,
  TouchableHighlight,
  SafeAreaView,
  Image,
} from "react-native";
import { FontAwesome } from "@expo/vector-icons";
import Colors from "../config/Colors";
import Server from "../config/Server";

const LoginScreen = ({ navigation }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const loginSearch = async () => {
    console.log(email);
    try {
      const responseSearch = await fetch(`${Server.api_url}/users/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email: email, password: password }),
      });
      if (!responseSearch.ok) {
        throw new Error("Zły status");
      }
      const responseDataSearch = await responseSearch.json();
      return responseDataSearch;
    } catch (error) {
      console.log("[Login screen] - function Login search");
    }
  };

  const handleLogin = async () => {
    if (password != "" && email != "") {
      const responseLogin = await loginSearch();
      if (responseLogin.answer == "Pomyślnie zalogowano") {
        navigation.navigate("User");
      } else {
        return setError(responseLogin);
      }
    } else {
      return setError({ answer: "Wszytkie pola są wymagane" });
    }
  };

  const handleRegister = () => {
    navigation.navigate("Register");
  };

  return (
    <SafeAreaView style={styles.container}>
      <SafeAreaView style={styles.form}>
        <SafeAreaView style={styles.inputContainer}>
          <FontAwesome name="envelope-o" style={styles.envelope} size={25} />
          <TextInput
            placeholder="Email"
            placeholderTextColor={Colors.placeholder}
            value={email}
            onChangeText={(text) => setEmail(text)}
            style={styles.input}
          />
        </SafeAreaView>
        <SafeAreaView style={styles.inputContainer}>
          <FontAwesome name="lock" size={25} style={styles.lock} />
          <TextInput
            placeholder="Hasło"
            r
            placeholderTextColor={Colors.placeholder}
            value={password}
            onChangeText={(text) => setPassword(text)}
            secureTextEntry
            style={styles.input}
          />
        </SafeAreaView>
        <TouchableHighlight style={styles.login} onPress={handleLogin}>
          <Text style={styles.buttonText}>Zaloguj</Text>
        </TouchableHighlight>
        <Text style={styles.errorText}>{error?.answer}</Text>
      </SafeAreaView>
      <SafeAreaView style={styles.registerContainer}>
        <Text style={styles.registerText}>Nie masz jeszcze konta?</Text>
        <TouchableHighlight onPress={handleRegister} style={styles.register}>
          <Text style={styles.buttonText}>Zarejestruj się</Text>
        </TouchableHighlight>
      </SafeAreaView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  text: {
    color: Colors.white,
  },
  inputContainer: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: 10,
  },
  lock: {
    color: Colors.white,
    marginRight: 15,
    paddingRight: 6,
  },
  envelope: {
    marginRight: 15,
    color: Colors.white,
  },
  container: {
    backgroundColor: Colors.secondary,
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  form: {
    width: "90%",
    alignItems: "center",
  },
  input: {
    marginBottom: 10,
    padding: 10,
    borderColor: "gray",
    borderWidth: 1,
    borderRadius: 10,
    width: "90%",
    color: Colors.white,
  },
  registerContainer: {
    position: "absolute",
    bottom: 20,
    alignItems: "center",
    width: "100%",
  },
  login: {
    backgroundColor: Colors.primary,
    color: Colors.white,
    borderRadius: 10,
    borderColor: Colors.primary,
    width: "100%",
    padding: 10,
  },
  register: {
    backgroundColor: Colors.primary,
    color: Colors.white,
    borderRadius: 10,
    borderColor: Colors.primary,
    width: "90%",
    padding: 10,
  },
  buttonText: {
    color: "white",
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
  },
  registerText: {
    color: Colors.white,
    marginBottom: 5,
  },
  errorText: {
    color: Colors.errorRed,
    marginTop: 10,
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
  },
});

export default LoginScreen;
