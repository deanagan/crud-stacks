import { FC, useEffect, useRef, memo } from "react";
import ReactDOM from "react-dom";
import { CSSTransition } from "react-transition-group";
import styled from "styled-components";
import { ButtonWrapper, CloseIcon } from "../design-system/atoms";

const StyledModal = styled.div`
  padding-top: 15%;
  position: fixed;
  left: 0;
  top: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
  opacity: 0;
  transition: all 0.3s ease-in-out;
  pointer-events: none;
  z-index: 10;

  &.enter-done {
    opacity: 1;
    pointer-events: visible;
  }

  &.exit {
    opacity: 0;
  }
`;

const ModalContent = styled.div`
  width: 600px;
  background-color: #fff;
  transition: all 0.3s ease-in-out;
  transform: translateY(-200px);
`;

const ModalHeader = styled.div`
  padding: 10px;
`;

const ModalFooter = styled.div`
  padding: 10px;
`;

const ModalTitle = styled.h4`
  margin: 0;
`;

const ModalClose = styled.div`
  float: right;
  cursor: pointer;
`;

const ModalBody = styled.div`
  padding: 10px;
  border-top: 1px solid #eee;
  border-bottom: 1px solid #eee;
`;

const CancelButton = styled(ButtonWrapper)`
  &&& {
    background-color: indianred;
    color: white;
  }
`;

const OkButton = styled(ButtonWrapper)`
  &&& {
    background: blue;
    color: white;
  }
`;

interface ModalProps {
  onCancel: () => void;
  onOk: () => void;
  showFooter: boolean;
  showClose: boolean;
  okText: string;
  cancelText: string;
  title: string;
  show: boolean;
}

export const Modal: FC<ModalProps> = memo(({
  onCancel,
  onOk,
  show,
  children,
  title,
  okText,
  cancelText,
  showFooter,
  showClose,
}) => {

  const portalRef = useRef(null);
  const callback = useRef<typeof onCancel>(onCancel);

  useEffect(() => {
    const closeWhenEscapeKeyPressed = (key: string) => {
      if (key === "Escape") {
        callback.current();
      }
    };

    document.body.addEventListener("keydown", (e) =>
      closeWhenEscapeKeyPressed(e.key)
    );

    return () => {
      document.body.removeEventListener("keydown", (e) =>
        closeWhenEscapeKeyPressed(e.key)
      );
    };
  }, []);

  const OkAndClose = () => {
    onOk();
    onCancel();
  };

  const footerStyle = showFooter ? "block" : "none";
  const closeStyle = showClose ? "block" : "none";

  return ReactDOM.createPortal(
    <CSSTransition nodeRef={portalRef} in={show} unmountOnExit timeout={{ enter: 0, exit: 300 }}>
      <StyledModal ref={portalRef} onClick={onCancel}>
        <ModalContent onClick={(e) => e.stopPropagation()}>
          <ModalHeader>
            <ModalTitle>
              {title}
              <ModalClose onClick={onCancel}>
                <CloseIcon style={{ display: closeStyle }} />
              </ModalClose>
            </ModalTitle>
          </ModalHeader>
          <ModalBody>{children}</ModalBody>
          <ModalFooter style={{ display: footerStyle }}>
            <OkButton onClick={OkAndClose}>{okText}</OkButton>
            <CancelButton onClick={onCancel}>{cancelText}</CancelButton>
          </ModalFooter>
        </ModalContent>
      </StyledModal>
    </CSSTransition>,
    document.getElementById("root") as HTMLElement
  );
});
