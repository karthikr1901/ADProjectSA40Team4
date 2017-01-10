package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by student on 16/9/15.
 */
public class POReceiveInfo {
    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<RequisitionFormInfo> parts = null;
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public static ArrayList<POVoucherNumber> list = new ArrayList<POVoucherNumber>();

    public String ItemID, Description, UOM,OrderDate,ExpectedDeliveryDate;
    public int OrderQty;

    public POReceiveInfo(String ItemID, String Description, String UOM, int OrderQty, String OrderDate, String ExpectedDeliveryDate) {
        this.ItemID = ItemID;
        this.Description = Description;
        this.UOM = UOM;
        this.OrderQty = OrderQty;
        this.OrderDate = OrderDate;
        this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    }

//    public POVoucherNumber(String voucherNumber) {
//        this.voucherNumber = voucherNumber;
//    }

    public static ArrayList<POReceiveInfo> getPODetailInfo(String POID) {
        ArrayList<POReceiveInfo> arrResult = new ArrayList<POReceiveInfo>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/MgetPurchaseOrderListbyPOid/%s", wcfUrl, POID));
            for (int i = 0; i < a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                arrResult.add(new POReceiveInfo(obj.getString("ItemID"), obj.getString("Description"), obj.getString("UOM"), obj.getInt("OrderQty"), obj.getString("OrderDate"), obj.getString("ExpectedDeliveryDate")));
            }
        } catch (Exception e) {
        }
        return arrResult;
    }

    public static ArrayList<POVoucherNumber> GetPONumbers() {
        ArrayList<POVoucherNumber> arrResult = new ArrayList<POVoucherNumber>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/MgetPurchaseOrderID", wcfUrl));
            for (int i = 0; i < a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                arrResult.add(new POVoucherNumber(obj.getString("PurchaseOrderID").toString()));
                String temp = obj.getString("PurchaseOrderID").toString();
                list.add(new POVoucherNumber(temp));
            }
        } catch (Exception e) {
        }
        return arrResult;
    }

//    public static ReturnMessage ApproveAdjustmentVoucher(String adjID, String empID) {
//        String urlStr = String.format("%s/approvedAdjustmentListMobile/%s/%s", wcfUrl, adjID, empID);
//        ReturnMessage Rmsg = null;
//        try {
//            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/approvedAdjustmentListMobile/%s/%s", wcfUrl, adjID, empID));
//
//            if (obj != null) {
//                Rmsg = new ReturnMessage(obj.getString("RMessage"));
//            }
//
//        } catch (Exception e) {
//        }
//        return  Rmsg;
//    }

    public static class POVoucherNumber {

        private String vNumber;

        public POVoucherNumber(String vNumber) {
            this.vNumber = vNumber;
        }

        @Override
        public String toString() {
            return vNumber;
        }
    }

    public static  class ReturnMessage {
        public  String msg;

        public ReturnMessage(String msg) {
            this.msg = msg;
        }
    }
}
