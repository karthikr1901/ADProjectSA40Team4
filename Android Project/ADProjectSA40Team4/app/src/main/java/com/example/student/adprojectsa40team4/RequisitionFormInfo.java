package com.example.student.adprojectsa40team4;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 7/9/15.
 */
public class RequisitionFormInfo extends HashMap<String, String>{

    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<RequisitionFormInfo> parts = null;
    public static String wcfUrl = ConfigurationManager.wcfUrl;

    public String itemid,uom,description;
    public int balance,tneeded,talloted;
    public static String Description, UnitOfMesurement;

    public RequisitionFormInfo(String itemid,String description, String uom, int balance, int tneeded, int talloted) {
        this.itemid = itemid;
        this.description = description;
        this.uom = uom;
        this.balance = balance;
        this.tneeded = tneeded;
        this.talloted = talloted;
    }


    public static ArrayList<RequisitionFormInfo> GetRequests(){
        ArrayList<RequisitionFormInfo> c = new ArrayList<RequisitionFormInfo>();

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl
                    (wcfUrl + "/GetRequests");
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                c.add(new RequisitionFormInfo(obj.getString("itemid"),obj.getString("Description"),obj.getString("uom"),Integer.parseInt(obj.getString("balance")),Integer.parseInt(obj.getString("tneeded")),Integer.parseInt(obj.getString("talloted"))));
            }
        } catch (Exception e) {
        }
        return c;
    }
//
//    public static ArrayList<RequisitionFormInfo> GetUpdateRequests(){
//        ArrayList<RequisitionFormInfo> c = new ArrayList<RequisitionFormInfo>();
//
//        try {
//            JSONArray a = JSONParser.getJSONArrayFromUrl(wcfUrl + "GetUpdateRequests");
//            for (int i=0; i<a.length(); i++) {
//                JSONObject obj = a.getJSONObject(i);
//                c.add(new RequisitionFormInfo(obj.getString("itemid"),obj.getString("uom"),Integer.parseInt(obj.getString("balance")),Integer.parseInt(obj.getString("tneeded")),Integer.parseInt(obj.getString("talloted"))));
//            }
//        } catch (Exception e) {
//        }
//        return c;
//    }

    public static ArrayList<RequisitionFormInfo> GetUpdateRequests(String itemid,String damageqty,String empID){
        ArrayList<RequisitionFormInfo> c = new ArrayList<RequisitionFormInfo>();
       // String urlStr = String.format( "%s/GetUpdateRequests/%s/%s",wcfUrl,itemid,damageqty);

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetUpdateRequests/%s/%s/%s",wcfUrl,itemid,damageqty,empID));
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                c.add(new RequisitionFormInfo(obj.getString("itemid"),obj.getString("Description"),obj.getString("uom"),Integer.parseInt(obj.getString("balance")),Integer.parseInt(obj.getString("tneeded")),Integer.parseInt(obj.getString("talloted"))));
            }
        } catch (Exception e) {
        }
        return c;

    }

    public static void MobileGetDetails(String ItemID) {
        Mrequest mr=null;

        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/MobileGetDetails/%s", wcfUrl, ItemID));
            //mr = new Mrequest(obj.getString("Description").toString(),Integer.parseInt(obj.getString("Quantity").toString()),obj.getString("UnitOfMeasurement").toString())
            Description = obj.getString("Description").toString();
            UnitOfMesurement = obj.getString("UnitOfMeasurement").toString();

        } catch (Exception e) {
            Log.e("MobileGetDetails", "JSON error");
        }
    }

    public static ArrayList<Mrequest> MobileAddItem(String RequestID, String ItemNo, String Quantity) {

        ArrayList<Mrequest> list = new ArrayList<Mrequest>();

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/MobileAddItem/%s/%s/%s", wcfUrl, RequestID, ItemNo, Quantity));
            for (int i =0; i<a.length(); i++) {

                JSONObject obj = a.getJSONObject(i);
                list.add(new Mrequest(ItemNo,obj.getString("Description").toString(),Integer.parseInt(obj.getString("Quantity").toString()),obj.getString("UnitOfMeasurement").toString()));

            }
        } catch (Exception e) {
            Log.e("MobileAddItem", "JSONArray error");
        }
        return list;
    }


    public static String MobileGenReqNoFirst(String EmployeeID) {
        String RID="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/MobileGenReqNoFirst/%s", wcfUrl, EmployeeID));
            RID =  obj.getString("RequestID").toString();

        } catch (Exception e) {
            Log.e("MobileGenReqNo", "JSON error");
        }
        return RID;
    }

    public static String MobileGenReqNoSecond(String EmployeeID) {
        String RID="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/MobileGenReqNoSecond/%s", wcfUrl, EmployeeID));
            RID =  obj.getString("RequestID").toString();

        } catch (Exception e) {
            Log.e("MobileGenReqNo", "JSON error");
        }
        return RID;
    }

    public static String DeleteItem(String ItemID, String RequestID) {
        String RMessage="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/DeleteItem/%s", wcfUrl, ItemID, RequestID));
            RMessage =  obj.getString("RMessage").toString();

        } catch (Exception e) {
            Log.e("DeleteItem", "JSON error");
        }
        return RMessage;
    }

    public static String MobileSaveReqNo(String RequestID) {
        String RMessage="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/MobileSaveReqNo/%s", wcfUrl, RequestID));
            RMessage =  obj.getString("RMessage").toString();

        } catch (Exception e) {
            Log.e("MobileSaveReqNo", "JSON error");
        }
        return RMessage;
    }

    public static ArrayList<Mrequest> MobViewEmpNewReq(String RequestID) {

        ArrayList<Mrequest> list = new ArrayList<Mrequest>();

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GridMobViewEmpNewReq/%s", wcfUrl, RequestID));
            for (int i =0; i<a.length(); i++) {

                JSONObject obj = a.getJSONObject(i);
                list.add(new Mrequest(obj.getString("ItemNo").toString(),obj.getString("Description").toString(),Integer.parseInt(obj.getString("Quantity").toString()),obj.getString("UnitOfMeasurement").toString()));

            }
        } catch (Exception e) {
            Log.e("MobViewEmpNewReq", "JSONArray error");
        }
        return list;
    }

    public static class Mrequest {

        public String ItemNo;
        public String Description;
        public int Quantity;
        public String UnitOfMeasurement;

        public Mrequest(String ItemNo,String Description, int Quantity, String UnitOfMeasurement) {
            this.ItemNo = ItemNo;
            this.Description = Description;
            this.Quantity = Quantity;
            this.UnitOfMeasurement = UnitOfMeasurement;
        }
    }




}
