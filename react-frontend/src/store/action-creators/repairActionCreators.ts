import { RepairActionTypes } from "../action-types/repairActionTypes";
import { Dispatch } from "redux";
import { Repair } from "../../types";
import { RepairAction } from "../actions/repairActions";
import { HttpClient } from "../action-apis/commonActionApi";

export const addRepair = (repair: Repair) => {
  return (dispatch: Dispatch<RepairAction>) => {
    new HttpClient().post<Repair>( {url: 'http://localhost:1337/repairs', requiresToken: false, payload: repair})
      .then((repair) => {
        dispatch({
          type: RepairActionTypes.ADD_REPAIR_ENTRY,
          repair: { ...repair, id: repair.id }
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };
};

export const updateRepairState = (id: number, fixed: boolean) => {
  const fixedState = { fixed: fixed };
  return (dispatch: Dispatch<RepairAction>) => {
    new HttpClient().put<Repair | typeof fixedState>({url: `http://localhost:1337/repairs/${id}`, requiresToken: false, payload: fixedState} )
    .then((data) => {
      const repair = data as Repair;
      dispatch({
        type: RepairActionTypes.UPDATE_REPAIR_STATE,
        id: repair.id as number,
        fixed: repair.fixed,
      });
    });
  };
};

export const deleteRepairEntry = (id: number) => {
  return (dispatch: Dispatch<RepairAction>) => {
    new HttpClient().delete<Repair | number>({url: `http://localhost:1337/repairs/${id}`, requiresToken: false, payload: id} )
    .then((data) => {
      const repair = data as Repair;
      dispatch({
        type: RepairActionTypes.DELETE_REPAIR_ENTRY,
        id: repair.id as number,
        fixed: repair.fixed,
      });
    });
  };
};

export const getRepairEntries = () => {
  return (dispatch: Dispatch<RepairAction>) => {
    new HttpClient().get<Repair[]>( {url: 'http://localhost:1337/repairs', requiresToken: false})
    .then((repairs) => {
      dispatch({
        type: RepairActionTypes.GET_REPAIR_ENTRIES,
        repairs: repairs.map((repair: Repair) => {
          return {
            id: repair.id,
            name: repair.name,
            detail: repair.detail,
            fixed: repair.fixed,
          };
        }),
      });
    });
  };
};
