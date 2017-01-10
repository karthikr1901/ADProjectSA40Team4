package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 11/9/15.
 */
public class DisbursementInfo extends HashMap<String, String> {
    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<CollectionPointInfo> parts = null;
    public static String wcfUrl = ConfigurationManager.wcfUrl;

    public String description,UOM;
    public int outstandingqty,recievedqty,requestedqty;

    public DisbursementInfo(String description, int outstandingqty,int recievedqty,int requestedqty,String UOM) {
        this.description = description ;
        this.outstandingqty = outstandingqty;
        this.recievedqty = recievedqty;
        this.requestedqty = requestedqty;
        this.UOM = UOM;
    }

    public static ArrayList<DisbursementInfo> GetDisbursementInfoList(String deptid){
        ArrayList<DisbursementInfo> d = new ArrayList<DisbursementInfo>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/DisburseDepartGrid/%s",wcfUrl,deptid));
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                d.add(new DisbursementInfo(obj.getString("description"),Integer.parseInt(obj.getString("outstandingqty")),Integer.parseInt(obj.getString("recievedqty")),Integer.parseInt(obj.getString("requestedqty")),obj.getString("UnitOfMeasurement")));
            }
        } catch (Exception e) {
        }
        return  d;
    }

    public static Integer UpdateDisburse(String deptid,String empID){
        String urlStr = String.format("%s/updateDisburse/%s/%s",wcfUrl,deptid);
        Integer ret = 1;
        try {
            String result = JSONParser.postStream(urlStr,empID);
           // JSONObject obj = JSONParser.getJSONFromUrl(String.format( "%s/updateDisburse/%s/%s",wcfUrl,deptid,empID));
            if (result != null)
                ret = 1;
            else return
                ret = 0;

        } catch (Exception e) {
        }
        return ret;
    }
}
