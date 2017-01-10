package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.Fragment;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.student.adprojectsa40team4.dummy.dummy.DummyContent;

import java.util.ArrayList;
import java.util.HashMap;

public class InventoryStatusFragment extends Fragment implements AbsListView.OnItemClickListener,BackHandledFragment.BackHandlerInterface {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private OnFragmentInteractionListener mListener;

    private AbsListView mListView;
    ListView list_data, list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading, map_list;
    private ListAdapter mAdapter;
    private BackHandledFragment selectedFragment;
    TextView totalCount;
    Activity getAct;

    // TODO: Rename and change types of parameters
    public static InventoryStatusFragment newInstance(String param1, String param2) {
        InventoryStatusFragment fragment = new InventoryStatusFragment();
        Bundle args = new Bundle();
        fragment.setArguments(args);
        return fragment;
    }

    public InventoryStatusFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
        }

        // TODO: Change Adapter to display your content
        mAdapter = new ArrayAdapter<DummyContent.DummyItem>(getActivity(),
                android.R.layout.simple_list_item_1, android.R.id.text1, DummyContent.ITEMS);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        super.onCreateView(inflater,container,savedInstanceState);
        View view = inflater.inflate(R.layout.iventory_status_fragment, container, false);
        totalCount = (TextView)view.findViewById(R.id.totalCount);
        list_heading = (ListView) view.findViewById(R.id.list_heading);
        list_data = (ListView) view.findViewById(R.id.list_data);
        PopulateList();
        return view;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    public void PopulateList(){
        mylist = new ArrayList<HashMap<String, String>>();
        mylist_title = new ArrayList<HashMap<String, String>>();

        map_heading = new HashMap<String, String>();

        map_heading.put("itemCode", "Item Code");
        map_heading.put("qtyOnHand", "Qty On Hand");
        map_heading.put("reOrderLevel", "Reorder Level");
        map_heading.put("suggestQty", "Sugges Qty");
        mylist_title.add(map_heading);

        adapter_title = new SimpleAdapter(getActivity(),
                mylist_title,
                R.layout.request_row,
                new String[] {"itemCode", "qtyOnHand", "reOrderLevel", "suggestQty"},
                new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4});
        list_heading.setAdapter(adapter_title);

        getAct = getActivity();

        new AsyncTask<Void, Void, ArrayList<InventoryStatusItemInfo>>() {
            @Override
            protected ArrayList<InventoryStatusItemInfo> doInBackground(Void... params) {
                ArrayList<InventoryStatusItemInfo> invInfoList = InventoryStatusItemInfo.DisplayLowLevelStock();
                return invInfoList;
            }

            @Override
            protected void onPostExecute(ArrayList<InventoryStatusItemInfo> invInfoList) {
                if (invInfoList.size() > 0) {
                    for (int i = 0; i < invInfoList.size(); i++) {
                        InventoryStatusItemInfo invInfo = invInfoList.get(i);
                        map_list = new HashMap<String, String>();
                        map_list.put("itemCode", String.valueOf(invInfo.ItemID));
                        map_list.put("qtyOnHand", String.valueOf(invInfo.Balance));
                        map_list.put("reOrderLevel", String.valueOf(invInfo.ReorderLevel));
                        map_list.put("suggestQty", String.valueOf(invInfo.SuggestedQuantity));
                        mylist.add(map_list);
                    }

                    adapter = new SimpleAdapter(getAct,
                            mylist,
                            R.layout.inventory_status_row,
                            new String[] {"itemCode", "qtyOnHand", "reOrderLevel", "suggestQty"},
                            new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4});
                    list_data.setAdapter(adapter);

                   // int txtTotalCount = mylist.size();
                    totalCount.setText("Total Count : " + String.valueOf(mylist.size()));
                } else {
                    Toast.makeText(InventoryStatusFragment.this.getActivity(), getString(R.string.NoLowStock), Toast.LENGTH_SHORT).show();
                }
            }
        }.execute();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        if (null != mListener) {
            // Notify the active callbacks interface (the activity, if the
            // fragment is attached to one) that an item has been selected.
            mListener.onFragmentInteraction(DummyContent.ITEMS.get(position).id);
        }
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        public void onFragmentInteraction(String id);
    }


    public void setEmptyText(CharSequence emptyText) {
        View emptyView = mListView.getEmptyView();

        if (emptyView instanceof TextView) {
            ((TextView) emptyView).setText(emptyText);
        }
    }

    @Override
    public void setSelectedFragment(BackHandledFragment backHandledFragment) {
        this.selectedFragment = selectedFragment;
    }
}
