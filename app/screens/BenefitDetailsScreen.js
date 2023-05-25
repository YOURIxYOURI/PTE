import React, { useState, useEffect } from "react";
import {
  View,
  StyleSheet,
  SafeAreaView,
  Text,
  TouchableWithoutFeedback,
  NativeModules,
  Dimensions,
} from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { useAtomValue } from "jotai";
import { benefitIdAtom } from "../store/AuthAtom";
import QRCode from "react-native-qrcode-svg";
const { StatusBarManager } = NativeModules;

const STATUSBAR_HEIGHT = Platform.OS === "ios" ? 20 : StatusBarManager.HEIGHT;

const BenefitDetailsScreen = ({ navigation }) => {
  const [benefit, SetBenefit] = useState(null);
  const [loading, setLoading] = useState(false);
  const [ifQr, setIfQr] = useState(true);
  const ID = useAtomValue(benefitIdAtom);

  const getBenefit = async (id) => {
    try {
      setLoading(false);
      const responseSearch = await fetch(
        `${Server.api_url}/benefits-to-user/benefit`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: id }),
        }
      );
      if (!responseSearch.ok) {
        throw new Error("Zły status");
      }
      const responseDataSearch = await responseSearch.json();
      return SetBenefit(responseDataSearch.benefit);
    } catch (error) {
      console.log("[BenefitDetails screen] - function getBenefit");
    } finally {
      setLoading(true);
    }
  };

  useEffect(() => {
    getBenefit(ID);
  }, []);

  return (
    <SafeAreaView style={styles.container}>
      {loading && (
        <View style={styles.contentCointaner}>
          <Text style={styles.headerText}>{benefit.name}</Text>
          <Text style={styles.descriptionText}>{benefit.description}</Text>
          <View style={styles.QRContainer}>
            {ifQr ? (
              <QRCode size={200} value={benefit.qrKey} />
            ) : (
              <Text style={styles.qrText}>{benefit.qrKey}</Text>
            )}
            <TouchableWithoutFeedback
              onPress={() => {
                ifQr ? setIfQr(false) : setIfQr(true);
              }}
              style={styles.qrChangeButton}
            >
              <Text style={styles.buttonText}>Zamień na tekst</Text>
            </TouchableWithoutFeedback>
          </View>
        </View>
      )}
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
  headerText: {
    textAlign: "left",
    fontSize: 35,
    fontWeight: "bold",
    color: Colors.white,
    paddingTop: 0,
  },
  qrText: {
    fontSize: 20,
    marginTop: 5,
    color: Colors.white,
  },
  descriptionText: {
    textAlign: "left",
    fontSize: 20,
    marginTop: 5,
    color: Colors.placeholder,
  },
  contentCointaner: {
    marginTop: STATUSBAR_HEIGHT,
    height: Dimensions.get("screen").height,
    width: Dimensions.get("screen").width,
    flex: 1,
    paddingTop: 30,
    paddingLeft: 20,
  },
  buttonText: {
    marginTop: 40,
    color: Colors.primary,
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
    textDecorationLine: "underline",
  },
  QRContainer: {
    flex: 1,
    justifyContent: "flex-start",
    paddingTop: 30,
    alignItems: "center",
  },
});

export default BenefitDetailsScreen;
