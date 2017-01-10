package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 7/9/15.
 */
public class CollectionPointInfo extends HashMap<String, String> {
    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<CollectionPointInfo> parts = null;
    public static String wcfUrl = ConfigurationManager.wcfUrl;

    public String CollectionPointID,Place,Time,InCharge;

    public CollectionPointInfo(String CollectionPointID, String Place,String Time,String InCharge) {
        this.CollectionPointID = CollectionPointID ;
        this.Place = Place;
        this.Time = Time;
        this.InCharge = InCharge;
    }

    public static ArrayList<CollectionPointInfo> CollectionPointList(String empid){
        ArrayList<CollectionPointInfo> c = new ArrayList<CollectionPointInfo>();


        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetCollectionPoints/%s",wcfUrl,empid));
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                c.add(new CollectionPointInfo(obj.getString("CollectionPointID"),obj.getString("Place"),obj.getString("Time"),obj.getString("InCharge")));

            }
        } catch (Exception e) {
        }
        return  c;
    }

}
