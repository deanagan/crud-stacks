import styled from "styled-components";
import { useCallback, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { ViewBox, Button } from "../design-system/atoms";
import { State, todoActionCreators } from "../store";
import { uuidv4Type } from "../types";
import {
  Modal,
} from "../components";
import { AddEntryForm } from "../components/addentryform";
import { TodoTable } from "../components/todoTable";
import { StatCard } from "../design-system/molecules";

const Wrapper = styled(ViewBox)`
  justify-content: center;
  background-color: ${({ theme }) => theme.Colors.white};
  margin-top: 55px;
`;

export const Home = () => {
  const dispatch = useDispatch();
  const [showAddModal, setShowAddModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [idForDeletion, setIdForDeletion] = useState<uuidv4Type | null>(null);

  const [newSummary, setNewSummary] = useState("");
  const [newDetail, setNewDetail] = useState("");

  const { addTodo, deleteTodo } =
    bindActionCreators(todoActionCreators, dispatch);

  const { todos } = useSelector((state: State) => state.todo);

  const cancelDeletion = () => {
    setIdForDeletion(null);
    setShowDeleteModal(false);
  };

  const addEntryFormProp = {
    summary: newSummary,
    detail: newDetail,
    changeSummary: setNewSummary,
    changeDetail: setNewDetail,
  };

  const onCancel = useCallback(() => {
    setShowAddModal(false);
    setNewDetail("");
    setNewSummary("");
  }, []);

  // const showAddModalHandler = () => setShowAddModal(true);

  const addEntryForm = <AddEntryForm {...addEntryFormProp} />;
  return (
    <Wrapper w={80} h={100}>
      <StatCard totalTasks={todos.length} totalIncompleteTasks={todos.filter((todo) => !todo.isDone).length} />
      <TodoTable todos={todos} />
      <Button onClick={() => setShowAddModal(true)}>Add Todo</Button>
      <Modal
        onCancel={onCancel}
        onOk={() => {
          setShowAddModal(false);
          addTodo({ summary: newSummary, detail: newDetail, isDone: false });
        }}
        show={showAddModal}
        title="Add New Todo"
        okText="Ok"
        cancelText="Cancel"
        children={addEntryForm}
        allowOk={!!(newSummary && newDetail)}
        showClose
      />
      <Modal
        onCancel={() => cancelDeletion()}
        onOk={() => idForDeletion !== null && deleteTodo(idForDeletion)}
        show={showDeleteModal}
        title="Delete Todo"
        okText="Ok"
        cancelText="Cancel"
        children={<h4>Are you sure you want to delete this request?</h4>}
        showClose={false}
      />
    </Wrapper>
  );
};
