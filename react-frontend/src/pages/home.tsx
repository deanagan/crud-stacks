import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import styled from "styled-components";
import { ActionLink } from "../components/ActionLink";
import { ViewBox, Button } from "../design-system/atoms";
import { actionCreators, State } from "../store";
import { ToggleSwitch } from "../components/ToggleSwitch";
import { Table } from "../components/Table";
import { Modal } from "../components/Modal";
import { AddEntryForm } from "../components/AddEntryForm";

const Wrapper = styled(ViewBox)`
  justify-content: center;
  background-color: ${({ theme }) => theme.Colors.white};
  margin-top: 55px;
`;

export const Home = () => {
  const dispatch = useDispatch();
  const { tasks } = useSelector((state: State) => state.task);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [idForDeletion, setIdForDeletion] = useState<number | null>(null);

  const [newName, setNewName] = useState("");
  const [newDetail, setNewDetail] = useState("");

  const { addTask, deleteTask } = bindActionCreators(actionCreators, dispatch);


  const deleteEntry = (id: number) => {
    setIdForDeletion(id);
    setShowDeleteModal(true);
  }

  const cancelDeletion = () => {
    setIdForDeletion(null);
    setShowDeleteModal(false);
  }

  const addEntryFormProp = {
    name: newName,
    detail: newDetail,
    changeName: setNewName,
    changeDetail: setNewDetail,
  }

  useEffect(() => {
    dispatch(actionCreators.getTasks());
  }, [dispatch]);

  return (
    <Wrapper w={600} h={600}>
      <Table
        rowData={tasks.map((task) => (
            {
                id: task.id as number,
                name: task.name,
                detail: task.detail,
                fixed: task.fixed ? "True" : "False",
                switch: (<ToggleSwitch switchId={task.id as number} fixed={task.fixed}/>),
                deleter: (<ActionLink color='red' message='delete' deleteFn={() => deleteEntry(task.id as number)}/>)
            }
          ))}
        columnLabels={['Name', 'Detail', 'Completed', 'Update', 'Remove Task']}
        rowFields={['name', 'detail', 'fixed', 'switch', 'deleter']}
      />

      <Button onClick={() => setShowAddModal(true)}>Add Request</Button>
      <Modal
        onCancel={() => {
          setShowAddModal(false);
          setNewDetail("");
          setNewName("");
        }}
        onOk={() => {
          setShowAddModal(false);
          addTask({name: newName, detail: newDetail, fixed: false});
        }}
        show={showAddModal}
        title="Add New Request"
        okText="Ok"
        cancelText="Cancel"
        children={<AddEntryForm {...addEntryFormProp} />}
        showFooter={!!(newName && newDetail)}
        showClose
       />
       <Modal
        onCancel={() => cancelDeletion()}
        onOk={() => idForDeletion !== null && deleteTask(idForDeletion)}
        show={showDeleteModal}
        title="Delete Request"
        okText="Ok"
        cancelText="Cancel"
        children={<h4>Are you sure you want to delete this request?</h4>}
        showFooter
        showClose={false}
       />
    </Wrapper>
  );
};
