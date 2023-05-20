import React, { useState } from "react";
import {
  SafeAreaView,
  TextInput,
  Button,
  Alert,
  StyleSheet,
  Text,
  TouchableHighlight,
} from "react-native";
import Colors from "../config/Colors";
import { FontAwesome } from "@expo/vector-icons";
import Server from "../config/Server";

const RegisterScreen = ({ navigation }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confpassword, setConfPassword] = useState("");
  const [error, setError] = useState("");

  const searchUser = async () => {
    console.log(email);
    try {
      const responseSearch = await fetch(`${Server.api_url}/users/data`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email: email }),
      });
      if (!responseSearch.ok) {
        throw new Error("Zły status");
      }
      const responseDataSearch = await responseSearch.json();
      return responseDataSearch;
    } catch (error) {
      console.log("[RegisterScreen] - function search user");
    }
  };
  const registerUser = async () => {
    console.log(email);
    console.log(password);
    console.log(confpassword);
    try {
      const responseRegister = await fetch(`${Server.api_url}/users/register`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
          confpassword: confpassword,
        }),
      });

      if (!responseRegister.ok) {
        throw new Error("Zły status");
      }

      const responseDataRegister = await responseRegister.json();
      console.log(responseDataRegister);
      return responseDataRegister;
    } catch (error) {
      console.log("[RegisterScreen] - function register user");
      console.log(error);
    }
  };

  const handleLogin = async () => {
    if (email != "" && password != "" && confpassword != "") {
      const response3 = await searchUser();
      if (response3.email) {
        const response2 = await registerUser();
        if (response2.answer == "Pomyślnie zarejestrowano") {
          handleRegister();
        } else {
          return setError(response2);
        }
      } else {
        return setError(response3);
      }
    } else {
      return setError({ answer: "Wszytkie pola są wymagane" });
    }
  };

  const handleRegister = () => {
    navigation.navigate("Login");
  };

  return (
    <SafeAreaView style={styles.container}>
      <SafeAreaView style={styles.form}>
        <SafeAreaView style={styles.inputContainer}>
          <FontAwesome name="envelope-o" style={styles.envelope} size={25} />
          <TextInput
            placeholder="Email"
            value={email}
            placeholderTextColor={Colors.placeholder}
            onChangeText={(text) => setEmail(text)}
            style={styles.input}
          />
        </SafeAreaView>
        <SafeAreaView style={styles.inputContainer}>
          <FontAwesome name="lock" size={25} style={styles.lock} />
          <TextInput
            placeholder="Hasło"
            placeholderTextColor={Colors.placeholder}
            value={password}
            onChangeText={(text) => setPassword(text)}
            secureTextEntry
            style={styles.input}
          />
        </SafeAreaView>
        <SafeAreaView style={styles.inputContainer}>
          <FontAwesome name="lock" size={25} style={styles.lock} />
          <TextInput
            placeholder="Potwierdź hasło"
            placeholderTextColor={Colors.placeholder}
            value={confpassword}
            onChangeText={(text) => setConfPassword(text)}
            secureTextEntry
            style={styles.input}
          />
        </SafeAreaView>
        <TouchableHighlight style={styles.login} onPress={handleLogin}>
          <Text style={styles.buttonText}>Zarejestruj</Text>
        </TouchableHighlight>
        <Text style={styles.errorText}>{error?.answer}</Text>
      </SafeAreaView>
      <SafeAreaView style={styles.registerContainer}>
        <Text style={styles.registerText}>Masz już konto?</Text>
        <TouchableHighlight onPress={handleRegister} style={styles.register}>
          <Text style={styles.buttonText}>Zaloguj się</Text>
        </TouchableHighlight>
      </SafeAreaView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  text: {
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
  errorText: {
    color: Colors.errorRed,
    marginTop: 10,
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
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
});

export default RegisterScreen;
