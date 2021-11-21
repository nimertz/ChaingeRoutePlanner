import React from "react";
import {
  MapContainer,
  TileLayer,
  useMapEvents,
  MapConsumer
} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import L from "leaflet";
import icon from "./constants";
import "./styles.css";

export default function MyComponent() {
    const map = useMapEvents({
      click: (e) => {
        const { lat, lng } = e.latlng;
        L.marker([lat, lng], { icon }).addTo(map);
      }
    });

  return (
    <MapContainer
      center={[50.5, 30.5]}
      zoom={13}
      style={{ height: "100vh" }}
      // whenReady={(map) => {
      //   console.log(map);
      //   map.target.on("click", function (e) {
      //     const { lat, lng } = e.latlng;
      //     L.marker([lat, lng], { icon }).addTo(map.target);
      //   });
      // }}
    >
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      <MapConsumer>
        {(map) => {
          console.log("map center:", map.getCenter());
          map.on("click", function (e) {
            const { lat, lng } = e.latlng;
            L.marker([lat, lng], { icon }).addTo(map);
          });
          return null;
        }}
      </MapConsumer>
    </MapContainer>
  );
}
