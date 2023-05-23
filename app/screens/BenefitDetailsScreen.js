import React, { useState, useEffect } from "react";
import {
  View,
  StyleSheet,
  SafeAreaView,
  Text,
  TouchableHighlight,
} from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { useAtomValue } from "jotai";
import { benefitIdAtom } from "../store/AuthAtom";
import SvgQRCode from "react-native-qrcode-svg";

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
        <>
          <Text>{benefit.name}</Text>
          <Text>{benefit.description}</Text>
          {ifQr ? (
            <SvgQRCode value={benefit.qrKey} />
          ) : (
            <Text>{benefit.qrKey}</Text>
          )}
          <TouchableHighlight onPress={ifQr ? setIfQr(false) : setIfQr(true)}>
            <Text>Zamień na tekst</Text>
          </TouchableHighlight>
        </>
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
});

export default BenefitDetailsScreen;
