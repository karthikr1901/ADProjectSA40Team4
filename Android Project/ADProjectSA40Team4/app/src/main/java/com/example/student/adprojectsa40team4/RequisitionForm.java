package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 7/9/15.
 */
public class RequisitionForm extends HashMap<String, String>{

    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<RequisitionForm> parts = null;
    //public static String wcfUrl = "http://10.10.2.162/WCFFoodService/Service.svc/";
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public String Id,Name,Price,Shop;

    public RequisitionForm(String Id, String Name,String Shop,double Price) {
        put("Id", Id);
        put("Name", Name);
        put("Shop",Shop);
        put("Price", Double.toString(Price));
    }


    public static ArrayList<RequisitionForm> listCategory(){
        ArrayList<RequisitionForm> c = new ArrayList<RequisitionForm>();

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl
                    (wcfUrl + "GetAllCategory");
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                c.add(new RequisitionForm(obj.getString("Id"),obj.getString("Name"),obj.getString("ShopName"),Double.parseDouble(obj.getString("Price"))));

            }
        } catch (Exception e) {
        }
        return  c;
    }

    public static RequisitionForm GetCategory(String Id){
        RequisitionForm item = null;

        try {
            JSONObject obj = JSONParser.getJSONFromUrl(wcfUrl + "GetCategory/" + Id);
            item = new RequisitionForm(obj.getString("Id"),obj.getString("Name"),obj.getString("ShopName"),Double.parseDouble(obj.getString("Price")));
        } catch (Exception e) {
        }
        return  item;
    }
}
