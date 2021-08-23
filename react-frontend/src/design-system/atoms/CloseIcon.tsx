
import styled from 'styled-components';
import close from './close.svg';


export const ImageWrapper = styled.img`
  width: 24px;
  opacity: 0.6;
`

export const CloseIcon = ({...props}) => (
  <ImageWrapper
    src={close}
    alt="Close"
    {...props}
  />
);
