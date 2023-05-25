import React, { useState, useContext, useEffect } from "react";
import {
  SafeAreaView,
  StyleSheet,
  Text,
  Dimensions,
  View,
  TouchableHighlight,
} from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { FlashList } from "@shopify/flash-list";
import { useAtomValue } from "jotai";
import { userDataAtom } from "../store/AuthAtom";
import { Platform, NativeModules } from "react-native";
import { useSetAtom } from "jotai";
import { setIdAtom } from "../store/AuthAtom";
const { StatusBarManager } = NativeModules;

const STATUSBAR_HEIGHT = Platform.OS === "ios" ? 20 : StatusBarManager.HEIGHT;

const BenefitsScreen = ({ navigation }) => {
  const userData = useAtomValue(userDataAtom);
  const [benefits, SetBenefits] = useState(null);
  const [loading, setLoading] = useState(false);

  const getBenefits = async (id) => {
    try {
      setLoading(false);
      const responseSearch = await fetch(
        `${Server.api_url}/benefits-to-user/benefits`,
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
      console.log(responseDataSearch);
      return SetBenefits(responseDataSearch);
    } catch (error) {
      console.log("[Benefit screen] - function getBenefits");
    } finally {
      setLoading(true);
    }
  };

  useEffect(() => {
    getBenefits(userData.id);
  }, []);

  const set = useSetAtom(setIdAtom);
  // if (benefits.length === 0) return navigation.navigate("User");

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.listContainer}>
        {loading && (
          <FlashList
            data={benefits}
            estimatedItemSize={15}
            renderItem={({ item }) => {
              const benefit = item.benefit;
              return (
                <View style={styles.item} key={benefit.name}>
                  <TouchableHighlight
                    onPress={() => {
                      set(benefit.id);
                      navigation.navigate("BenefitDetails");
                    }}
                  >
                    <>
                      <Text
                        style={{
                          color: Colors.white,
                          fontSize: 20,
                          fontWeight: "bold",
                        }}
                      >
                        {benefit.name}
                      </Text>
                      <Text style={{ color: Colors.placeholder }}>
                        {benefit.description}
                      </Text>
                    </>
                  </TouchableHighlight>
                </View>
              );
            }}
          />
        )}
      </View>
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
  item: {
    marginBottom: 20,
    marginHorizontal: 20,
    padding: 10,
    borderRadius: 10,
    backgroundColor: Colors.primary,
  },
  listContainer: {
    marginTop: STATUSBAR_HEIGHT,
    height: Dimensions.get("screen").height,
    width: Dimensions.get("screen").width,
    flex: 1,
    paddingTop: 20,
  },
});

export default BenefitsScreen;
