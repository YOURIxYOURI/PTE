import React, { useState, useContext } from "react";
import { SafeAreaView, StyleSheet, Text, Dimensions, View } from "react-native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";
import { FlashList } from "@shopify/flash-list";
import { AuthContext } from "../components/AuthContext";

const BenefitsScreen = ({ navigation }) => {
  const { userData } = useContext(AuthContext);
  const [benefits, SetBenefits] = useState([]);

  const getBenefits = async (id) => {
    try {
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
      return SetBenefits(responseDataSearch);
    } catch (error) {
      console.log("[Benefit screen] - function getBenefits");
    }
  };

  getBenefits(userData.id);
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.listContainer}>
        <FlashList
          data={benefits}
          estimatedItemSize={15}
          renderItem={({ item }) => (
            <SafeAreaView style={styles.item}>
              <Text style={{ color: Colors.white, fontSize: 20 }}>
                {item.name}
              </Text>
              <Text style={{ color: Colors.placeholder }}>
                {item.description}
              </Text>
            </SafeAreaView>
          )}
        />
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
    padding: 10,
    borderRadius: 10,
    backgroundColor: Colors.primary,
  },
  listContainer: {
    height: Dimensions.get("screen").height,
    width: Dimensions.get("screen").width,
    flex: 1,
    paddingTop: 20,
  },
});

export default BenefitsScreen;
