import React from "react";
import { View, StyleSheet, SafeAreaView } from "react-native";
import { useRoute } from "@react-navigation/native";
import Colors from "../config/Colors";
import Server from "../config/Server";
import Navbar from "../components/Navbar";

const BenefitDetailsScreen = ({ navigation }) => {
  const [benefits, SetBenefits] = useState(null);
  const [loading, setLoading] = useState(false);
  const route = useRoute();
  const { id } = route.params;
  const ID = JSON.stringify(id);
  console.log("To jest moje id " + { ID });

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
      console.log(responseDataSearch);
      return SetBenefits(responseDataSearch);
    } catch (error) {
      console.log("[Benefit screen] - function getBenefits");
    } finally {
      setLoading(true);
    }
  };
  return (
    <SafeAreaView style={styles.container}>
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
